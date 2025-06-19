using System.ComponentModel.DataAnnotations;

namespace Backend.Common.Attributes;

public class FutureDateAttribute : ValidationAttribute {
    public override bool IsValid(object? value) {
        if (value is DateTime time) {
            return time > DateTime.Now;
        }

        return false;
    }
}