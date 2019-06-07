namespace MefContrib.Hosting.Conventions.Configuration
{
    using System;

    /// <summary>
    /// Defines a set of useful <see cref="ExportConventionBuilder{T}"/> extensions.
    /// </summary>
    public static class ExportConventionBuilderExtensions
    {
        public static ExportConventionBuilder<ExportConvention> ExportProperty(
            this ExportConventionBuilder<ExportConvention> builder, string propertyName)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            builder.Members(m => new[] { m.GetProperty(propertyName) });

            return builder;
        }
    }
}