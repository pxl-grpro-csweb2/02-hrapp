using Microsoft.AspNetCore.Identity;

namespace HrApp.Services
{
    public class IdentityServiceResult
    {
        private List<string> _errors = new List<string>();
        public IEnumerable<string> Errors => _errors;

        public IdentityUser IdentityUser { get; set; } = default!;
        public bool Succeeded { get; set; } = false;
        public void AddError(string error)
        {
            _errors.Add(error);
        }
    }
}
