using UniqueDraw.Domain.Attributes;
using UniqueDraw.Domain.Ports;

namespace UniqueDraw.Domain.Extensions;

public static class EncryptionExtensions
{
    public static void EncryptProperties<T>(this T entity, IEncryptionService encryptionService)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(encryptionService);

        var properties = typeof(T).GetProperties()
            .Where(p => Attribute.IsDefined(p, typeof(EncryptAttribute)) && p.CanRead && p.CanWrite);

        foreach (var property in properties)
        {
            var value = property.GetValue(entity) as string;
            if (!string.IsNullOrEmpty(value))
            {
                var encryptedValue = encryptionService.Encrypt(value);
                property.SetValue(entity, encryptedValue);
            }
        }
    }

    public static void DecryptProperties<T>(this T entity, IEncryptionService encryptionService)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(encryptionService);

        var properties = typeof(T).GetProperties()
            .Where(p => Attribute.IsDefined(p, typeof(EncryptAttribute)) && p.CanRead && p.CanWrite);

        foreach (var property in properties)
        {
            var value = property.GetValue(entity) as string;
            if (!string.IsNullOrEmpty(value))
            {
                var decryptedValue = encryptionService.Decrypt(value);
                property.SetValue(entity, decryptedValue);
            }
        }
    }
}
