terraform {
  required_providers {
    aws = {
      source = "hashicorp/aws"
      version = "~> 5.40.0"
    }
  }

  required_version = ">= 1.7.4"

  backend "s3" {
    bucket = "apitel-bucket"
    key = "infrastructure/state-files"
    region = "eu-west-1"
  }
}

provider "aws" {
  region = var.AWS_REGION
}

resource "aws_vpc" "apitel_vpc" {
  cidr_block = "10.0.0.0/16"
  enable_dns_support = true
  enable_dns_hostnames = true
  tags = {
    Name = "apitel_vpc"
  }
}

resource "aws_internet_gateway" "apitel_gateway" {
  vpc_id = aws_vpc.apitel_vpc.id
  tags = {
    owner: "liam.talberg@bbd.co.za"
  }
}

resource "aws_subnet" "apitel_subnet_a" {
  vpc_id                  = aws_vpc.apitel_vpc.id
  cidr_block              = "10.0.4.0/24"
  map_public_ip_on_launch = true
  availability_zone       = "eu-west-1a"
  tags = {
    owner: "liam.talberg@bbd.co.za"
  }
}

resource "aws_route_table" "apitel_route_table_a" {
  vpc_id = aws_vpc.apitel_vpc.id

  route {
    cidr_block = "0.0.0.0/0"
    gateway_id = aws_internet_gateway.apitel_gateway.id
  }

  tags = {
    owner: "liam.talberg@bbd.co.za"
  }
}

resource "aws_route_table_association" "apitel_association_a" {
  subnet_id      = aws_subnet.apitel_subnet_a.id
  route_table_id = aws_route_table.apitel_route_table_a.id
}

resource "aws_subnet" "apitel_subnet_b" {
  vpc_id                  = aws_vpc.apitel_vpc.id
  cidr_block              = "10.0.5.0/24"
  map_public_ip_on_launch = true
  availability_zone       = "eu-west-1b"
  tags = {
    owner: "liam.talberg@bbd.co.za"
  }
}

resource "aws_route_table" "apitel_route_table_b" {
  vpc_id = aws_vpc.apitel_vpc.id

  route {
    cidr_block = "0.0.0.0/0"
    gateway_id = aws_internet_gateway.apitel_gateway.id
  }

  tags = {
    owner: "liam.talberg@bbd.co.za"
  }
}

resource "aws_route_table_association" "apitel_association_b" {
  subnet_id      = aws_subnet.apitel_subnet_b.id
  route_table_id = aws_route_table.apitel_route_table_b.id
}

resource "aws_db_subnet_group" "apitel_subnet_group" {
  name       = "apitel_subnet_group"
  subnet_ids = [aws_subnet.apitel_subnet_a.id, aws_subnet.apitel_subnet_b.id]

  tags = {
    owner: "liam.talberg@bbd.co.za"
  }
}

resource "aws_security_group" "apitel_security_group" {
  vpc_id = aws_vpc.apitel_vpc.id

  ingress {
    from_port   = 5432
    to_port     = 5432
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }

  tags = {
    owner: "liam.talberg@bbd.co.za"
  }
}

resource "aws_db_instance" "apitel_sqlserver_rds" {
  identifier                  = "apitel-db-sqlserver"
  allocated_storage           = 20
  engine                      = "sqlserver-ex"
  engine_version              = "16.00.4095.4.v1"
  instance_class              = "db.t3.micro"
  publicly_accessible         = true
  username                    = var.DB_USERNAME
  password                    = var.DB_PASSWORD
  multi_az                    = false
  apply_immediately           = true
  copy_tags_to_snapshot       = true
  db_subnet_group_name        = aws_db_subnet_group.apitel_subnet_group.name
  skip_final_snapshot         = true
  vpc_security_group_ids      = [aws_security_group.apitel_security_group.id]
  
  tags = {
    owner = "liam.talberg@bbd.co.za"
  }
}

