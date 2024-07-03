yum install -y aws-cli && \
aws configure set aws_access_key_id test && \
aws configure set aws_secret_access_key test && \
aws configure set region us-east-1 && \
aws configure set output json && \

aws --endpoint-url=http://localstack-main:4566 sqs create-queue --queue-name sqs-outgoing-payments
aws --endpoint-url=http://localstack-main:4566 sqs create-queue --queue-name sqs-deposits-pending-verification
