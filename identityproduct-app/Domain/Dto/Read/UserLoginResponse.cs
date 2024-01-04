using System.Text.Json.Serialization;

namespace identityproduct_app.Domain.Dto.Read
{
    public class UserLoginResponse
    {
        public bool Success { get; private set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Token { get; private set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? ExpirationDate { get; private set; }
        public List<string> Errors { get; private set; }

        public UserLoginResponse() =>
            Errors = new List<string>();

        public UserLoginResponse(bool success = true) :this() =>
            Success = success;
        public UserLoginResponse(bool success, string token, DateTime? expirationDate) : this(success)
        {
            Token = token;
            ExpirationDate = expirationDate;
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        public void AddErrors(IEnumerable<string> errors)
        {
            Errors.AddRange(errors);
        }
    }
}
