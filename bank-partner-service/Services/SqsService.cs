using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.Internal.Transform;
using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace BankPartnerService.Services
{
    public class SqsService
    {
        private readonly AmazonSQSClient sqsClient;

        private readonly string outgoingPaymentsQueueUrl;
        private readonly string depositsPendingVerificationQueueUrl;

        public SqsService()
        {
            var accessKey = Environment.GetEnvironmentVariable("SQS_ACCESS_KEY");
            var secretKey = Environment.GetEnvironmentVariable("SQS_SECRET_KEY");
            var serviceUrl = Environment.GetEnvironmentVariable("SQS_SERVICE_URL");

            var credentials = new BasicAWSCredentials(accessKey, secretKey);

            sqsClient =  new AmazonSQSClient(credentials, new AmazonSQSConfig{
                ServiceURL = serviceUrl,
            });

            outgoingPaymentsQueueUrl = Environment.GetEnvironmentVariable("SQS_OUTGOING_PAYMENTS")!;
            depositsPendingVerificationQueueUrl = Environment.GetEnvironmentVariable("SQS_DEPOSITS_PENDING_VERIFICATION")!;
        }

        public Task AddOutgoingPayment(string reference, long amountInMibiBBDough, int receivingBankId, string receivingAccountId, int personaId)
        {
            var bodyDictionary = new Dictionary<string, object>
            {
                { "reference", reference },
                { "amountInMibiBBDough", amountInMibiBBDough },
                { "receivingBankId", receivingBankId },
                { "receivingAccountId", receivingAccountId },
                { "personaId", personaId }
            };

            return sqsClient.SendMessageAsync(outgoingPaymentsQueueUrl, JsonSerializer.Serialize(bodyDictionary));
        }

        public Task AddDepositCaptured(string reference)
        {
            return sqsClient.SendMessageAsync(depositsPendingVerificationQueueUrl, reference);

        }
    }
}
