using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace GiftOfTheGivers.Functions;

public class Function1
{
    private readonly ILogger<Function1> _logger;

    public Function1(ILogger<Function1> logger)
    {
        _logger = logger;
    }

    [Function("Function1")]
    public async Task<IActionResult> Run(
        [HttpTrigger(
            AuthorizationLevel.Anonymous,
            "post")]
        HttpRequest req)
    {
        _logger.LogInformation(
            "Donation tax certificate function received a request.");

        try
        {
            // Read the donation information from the request body.
            string requestBody;

            using (var reader = new StreamReader(req.Body))
            {
                requestBody = await reader.ReadToEndAsync();
            }

            // Make sure the request contains some data.
            if (string.IsNullOrWhiteSpace(requestBody))
            {
                _logger.LogWarning(
                    "The request did not contain any donation information.");

                return new BadRequestObjectResult(
                    new
                    {
                        error = "Donation information is required."
                    });
            }

            // Convert the JSON request into a donation request object.
            var donation = JsonSerializer.Deserialize<DonationRequest>(
                requestBody,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            // Check that the donation information was successfully read.
            if (donation == null)
            {
                return new BadRequestObjectResult(
                    new
                    {
                        error = "Invalid donation information."
                    });
            }

            // Check that a valid donation amount was supplied.
            if (donation.Amount <= 0)
            {
                _logger.LogWarning(
                    "Invalid donation amount received.");

                return new BadRequestObjectResult(
                    new
                    {
                        error = "Donation amount must be greater than zero."
                    });
            }

            // Generate a dummy tax certificate number.
            string certificateNumber =
                "TAX-" +
                Guid.NewGuid()
                    .ToString("N")[..8]
                    .ToUpper();

            _logger.LogInformation(
                "Tax certificate {CertificateNumber} generated for donation amount {Amount}.",
                certificateNumber,
                donation.Amount);

            // Return the generated certificate information.
            return new OkObjectResult(
                new
                {
                    certificateNumber = certificateNumber,
                    amount = donation.Amount,
                    message = "Tax certificate generated successfully."
                });
        }
        catch (JsonException)
        {
            _logger.LogWarning(
                "The Function received invalid JSON.");

            return new BadRequestObjectResult(
                new
                {
                    error = "The request contains invalid JSON."
                });
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "An error occurred while generating the tax certificate.");

            return new StatusCodeResult(
                StatusCodes.Status500InternalServerError);
        }
    }

    // This represents the information sent to the Function
    // when a donation is processed.
    public class DonationRequest
    {
        public decimal Amount { get; set; }

        public string? DonorType { get; set; }

        public string? DonorName { get; set; }
    }
}