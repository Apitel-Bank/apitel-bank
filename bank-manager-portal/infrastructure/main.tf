terraform {
  required_providers {
    aws = {
      source = "hashicorp/aws"
      version = "~> 5.40.0"
    }
  }

  required_version = ">= 1.7.4"

  backend "s3" {
    bucket = "apitel-portal-state-bucket"
    key = "infrastructure/portal/state-files"
    region = "eu-west-1"
  }
}

provider "aws" {
  region = "eu-west-1"
}

resource "aws_vpc" "apitel_portal_vpc" {
  cidr_block = "10.0.0.0/16"
  enable_dns_support = true
  enable_dns_hostnames = true
  tags = {
    Name = "apitel_portal_vpc"
  }
}

resource "aws_internet_gateway" "apitel_portal_gateway" {
  vpc_id = aws_vpc.apitel_portal_vpc.id
  tags = {
    owner: "liam.talberg@bbd.co.za"
  }
}

resource "aws_subnet" "apitel_portal_subnet_a" {
  vpc_id                  = aws_vpc.apitel_portal_vpc.id
  cidr_block              = "10.0.4.0/24"
  map_public_ip_on_launch = true
  availability_zone       = "eu-west-1a"
  tags = {
    owner: "liam.talberg@bbd.co.za"
  }
}

resource "aws_route_table" "apitel_portal_route_table_a" {
  vpc_id = aws_vpc.apitel_portal_vpc.id

  route {
    cidr_block = "0.0.0.0/0"
    gateway_id = aws_internet_gateway.apitel_portal_gateway.id
  }

  tags = {
    owner: "liam.talberg@bbd.co.za"
  }
}

resource "aws_route_table_association" "apitel_portal_association_a" {
  subnet_id      = aws_subnet.apitel_portal_subnet_a.id
  route_table_id = aws_route_table.apitel_portal_route_table_a.id
}

resource "aws_subnet" "apitel_portal_subnet_b" {
  vpc_id                  = aws_vpc.apitel_portal_vpc.id
  cidr_block              = "10.0.5.0/24"
  map_public_ip_on_launch = true
  availability_zone       = "eu-west-1b"
  tags = {
    owner: "liam.talberg@bbd.co.za"
  }
}

resource "aws_route_table" "apitel_portal_route_table_b" {
  vpc_id = aws_vpc.apitel_portal_vpc.id

  route {
    cidr_block = "0.0.0.0/0"
    gateway_id = aws_internet_gateway.apitel_portal_gateway.id
  }

  tags = {
    owner: "liam.talberg@bbd.co.za"
  }
}

resource "aws_route_table_association" "apitel_portal_association_b" {
  subnet_id      = aws_subnet.apitel_portal_subnet_b.id
  route_table_id = aws_route_table.apitel_portal_route_table_b.id
}

resource "aws_db_subnet_group" "apitel_portal_subnet_group" {
  name       = "apitel_portal_subnet_group"
  subnet_ids = [aws_subnet.apitel_portal_subnet_a.id, aws_subnet.apitel_portal_subnet_b.id]

  tags = {
    owner: "liam.talberg@bbd.co.za"
  }
}

#As the role already exists
data "aws_iam_role" "existing_role" {
  name = "aws-elasticbeanstalk-ec2"
}

#As the istance profile already exists
data "aws_iam_instance_profile" "beanstalk_ec2" {
  name = "aws-apitel-ec2-profile"
}

resource "aws_security_group" "apitel_portal_security_group" {
  vpc_id = aws_vpc.apitel_portal_vpc.id

  tags = {
    owner: "liam.talberg@bbd.co.za"
  }
}

resource "aws_vpc_security_group_egress_rule" "allow_all_traffic_ipv4" {
  security_group_id = aws_security_group.apitel_portal_security_group.id
  cidr_ipv4         = "0.0.0.0/0"
  ip_protocol       = "-1" # semantically equivalent to all ports
}

resource "aws_vpc_security_group_ingress_rule" "allow_tls_ipv4" {
  security_group_id = aws_security_group.apitel_portal_security_group.id
  cidr_ipv4         = aws_vpc.apitel_portal_vpc.cidr_block
  from_port         = 443
  ip_protocol       = "tcp"
  to_port           = 443
}

resource "aws_elastic_beanstalk_application" "app" {
  name        = "ApitelPortal"
  description = "Apitel Portal"
}

resource "aws_elastic_beanstalk_environment" "environment" {
  name        = "ApitelPortalEnv"
  application = aws_elastic_beanstalk_application.app.name
  solution_stack_name = "64bit Amazon Linux 2023 v6.1.6 running Node.js 20"

  setting {
    namespace = "aws:ec2:vpc"
    name      = "VPCId"
    value     = aws_vpc.apitel_portal_vpc.id
  }

  setting {
    namespace = "aws:autoscaling:asg"
    name      = "MinSize"
    value     = "1"
  }

  setting {
    namespace = "aws:autoscaling:asg"
    name      = "MaxSize"
    value     = "3"
  }

  setting {
    namespace = "aws:elasticbeanstalk:environment"
    name      = "EnvironmentType"
    value     = "LoadBalanced"
  }

  setting {
    namespace = "aws:ec2:vpc"
    name      = "Subnets"
    value     = join(",", aws_db_subnet_group.apitel_portal_subnet_group.subnet_ids)
  }

  setting {
    namespace = "aws:elasticbeanstalk:command"
    name      = "IgnoreHealthCheck"
    value     = "true"
  }

  setting {
    namespace = "aws:autoscaling:launchconfiguration"
    name      = "IamInstanceProfile"
    value     = data.aws_iam_instance_profile.beanstalk_ec2.name
  }
}

