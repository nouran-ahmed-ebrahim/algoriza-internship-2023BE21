using Core.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredAttributeForDoctor : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var httpContextAccessor = validationContext.GetService<IHttpContextAccessor>();
            var userManager = validationContext.GetService<UserManager<ApplicationUser>>();

            // Get the current user's ID unless it's null
            var currentUser = httpContextAccessor.HttpContext?.User;
            var userId = userManager.GetUserId(currentUser);

            // Check if the user has the "Admin" role
            bool isAdmin = false;
            if (!string.IsNullOrEmpty(userId))
            {
                string? CurrentRole = Enum.GetName(UserRole.Admin);
                var user = userManager.FindByIdAsync(userId).GetAwaiter().GetResult();
                isAdmin = user != null && userManager.IsInRoleAsync(user, CurrentRole).GetAwaiter().GetResult();
            }

            // If the user is a doctor, the property is required
            if (isAdmin)
            {
                if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }

}
