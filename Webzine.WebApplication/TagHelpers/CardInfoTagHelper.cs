// <copyright file="CardInfoTagHelper.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;

    /// <summary>
    /// TagHelper permettant de générer une card d'information avec des icônes, un nombre et une description.
    /// </summary>
    [HtmlTargetElement("card-info", Attributes = "class, f-color, for, sub-for")]
    public class CardInfoTagHelper : TagHelper
    {
        /// <summary>
        /// Obtient ou définit la classe CSS pour l'icône (ex: "fa-solid fa-users").
        /// </summary>
        [HtmlAttributeName("class")]
        public string IconClass { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit la couleur CSS pour le texte (ex: "text-primary").
        /// </summary>
        [HtmlAttributeName("f-color")]
        public string TextColor { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit la valeur principale affichée (ex: nombre d'artistes).
        /// </summary>
        [HtmlAttributeName("for")]
        public string For { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit le texte secondaire affiché sous la valeur principale (ex: "artistes").
        /// </summary>
        [HtmlAttributeName("sub-for")]
        public string SubFor { get; set; } = string.Empty;

        /// <summary>
        /// Méthode principale appelée pour transformer l'élément HTML ciblé par le TagHelper.
        /// </summary>
        /// <param name="context">
        /// Instance de <see cref="TagHelperContext"/> contenant les informations sur l'élément HTML transformé,
        /// notamment ses attributs et son contexte unique.
        /// </param>
        /// <param name="output">
        /// Instance de <see cref="TagHelperOutput"/> représentant le contenu HTML généré par le Tag Helper.
        /// Permet de modifier le tag HTML final en ajoutant des attributs ou du contenu.
        /// </param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Remplace le tag original par un div
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "col");

            // Génère le contenu HTML
            output.Content.SetHtmlContent($@"
                <div class='card card-cover h-100 overflow-hidden bg-light border-0 text-center'>
                    <p>
                        <i class='{this.IconClass} display-1 {this.TextColor} mb-3 mt-4'></i>
                        <h3 class='card-title {this.TextColor} mb-2'>{this.For}</h3>
                        <h6 class='{this.TextColor}'>{this.SubFor}</h6>
                    </p>
                </div>");
        }
    }
}