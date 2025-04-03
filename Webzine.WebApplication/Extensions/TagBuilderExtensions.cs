// <copyright file="TagBuilderExtensions.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Extensions
{
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// représentant une extention sur le contenu HTML généré par le Tag Helper.
    /// </summary>
    internal static class TagBuilderExtensions
    {
        /// <summary>
        /// Récupère la chaîne de caractère à l'intérieur du tagHelper.
        /// </summary>
        /// <param name="tagBuilder">Représente un build de tag.</param>
        /// <returns>Une string du contenu du tagHelper.</returns>
        public static string GetString(this TagBuilder tagBuilder)
        {
            using var writer = new System.IO.StringWriter();
            tagBuilder.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}