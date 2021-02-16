using System;
using Microsoft.Extensions.Configuration;

namespace Thinktecture.Samples.BASTA.Configuration.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IConfigurationExtensions
    {

        public static BastaAPIConfig GetBastaAPIConfig(this IConfiguration configuration)
        {
            var rootSection = configuration.GetSection(BastaAPIConfig.RootSectionName);
            if (rootSection == null)
                throw new ApplicationException(
                    $"Root Section ({BastaAPIConfig.RootSectionName}) not found in configuration");

            var section = rootSection.GetSection(BastaAPIConfig.SectionName);
            if (section == null)
                throw new ApplicationException(
                    $"Section ({BastaAPIConfig.SectionName}) not found in root section ({BastaAPIConfig.RootSectionName})");

            var config = new BastaAPIConfig();
            section.Bind(config);
            return config;

        }
        
    }
}
