// <copyright file="TitrePopulaireTagHelper.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.TagHelpers
{
    using System;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    /// <summary>
    /// TagHelper permettant d'afficher la card d'un titre populaire à partir du modèle lié via l'attribut asp-for.
    /// </summary>
    [HtmlTargetElement("titre-populaire", Attributes = "asp-for")]
    public class TitrePopulaireTagHelper : TagHelper
    {
        /// <summary>
        /// Obtient ou définit la liaison des propriétés du modèle via l'attribut asp-for.
        /// </summary>
        [HtmlAttributeName("asp-for")]
        public ModelExpression? For { get; set; }

        /// <summary>
        /// Méthode principale appelée pour transformer l'élément HTML ciblé par le TagHelper.
        /// </summary>
        /// <param name="context">
        /// Instance de <see cref="TagHelperContext"/> contenant les informations sur l'élément HTML transformé,
        /// notamment ses attributs et son contexte unique.
        /// </param>
        /// <param name="output">
        /// Instance de <see cref="TagHelperOutput"/> représentant le contenu HTML généré par le TagHelper.
        /// Permet de modifier le tag HTML final en ajoutant des attributs ou du contenu.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Levée si le modèle lié via asp-for est null ou n'a pas pu être récupéré.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Levée si le modèle lié à asp-for n'est pas valide ou ne correspond pas au type attendu.
        /// </exception>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Vérification que le modèle est correctement lié via asp-for
            if (this.For == null)
            {
                throw new InvalidOperationException("Le modèle n'a pas pu être récupéré.");
            }

            // Récupération du modèle lié
            var titre = this.For.Model as dynamic;

            // Vérification que le modèle est valide
            if (titre == null)
            {
                throw new ArgumentException("Le modèle lié à 'asp-for' est null ou invalide.");
            }

            // Transformation de l'élément HTML en une card Bootstrap
            output.TagName = "div"; // Remplace le tag original par un div
            output.Attributes.SetAttribute("class", "col"); // Ajoute une classe Bootstrap pour la mise en page

            // Génération du contenu HTML de la card
            output.Content.SetHtmlContent($@"
            <div class='card'>
                <div class='ratio ratio-1x1'>
                    <a asp-controller='Titre' asp-action='Index' asp-route-id='{titre.IdTitre}' class='h-100'>
                        <img src='{titre.UrlJaquette}' alt='jaquette' class='card-img-top object-fit-cover h-100 w-100'>
                    </a>
                </div>
                <div class='card-body'>
                    <h5 class='card-title mb-2'>
                        <a asp-controller='Titre' asp-action='Index' asp-route-id='{titre.IdTitre}' class='text-decoration-none'>{titre.Libelle}</a>
                    </h5>
                    <p class='card-text mb-0 text-muted'>
                        par
                        <a asp-controller='Artiste' asp-action='Index' asp-route-artiste='{titre.Artiste.Nom}' class='text-decoration-none'>{titre.Artiste.Nom}</a>
                    </p>
                </div>
            </div>");
        }
    }
}