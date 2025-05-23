﻿// <copyright file="FormChampTagHelper.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.TagHelpers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Webzine.WebApplication.Extensions;

    /// <summary>
    /// Tag Helper permettant de générer un champ de formulaire avec son label et son message de validation.
    /// </summary>
    /// <remarks>
    /// Initialise une nouvelle instance de la classe <see cref="FormChampTagHelper"/>.
    /// </remarks>
    /// <param name="htmlGenerator">Générateur HTML utilisé pour créer les balises.</param>
    [HtmlTargetElement("form-champ", Attributes = "asp-for")]
    public class FormChampTagHelper(IHtmlGenerator htmlGenerator) : TagHelper
    {
        private readonly IHtmlGenerator htmlGenerator = htmlGenerator;

        /// <summary>
        /// Obtient ou définit la liaison des propriétés du modèle via asp-for.
        /// </summary>
        [HtmlAttributeName("asp-for")]
        public ModelExpression? For { get; set; }

        /// <summary>
        /// Obtient ou définit le contexte de la vue Razor.
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; } = default!;

        /// <summary>
        /// Méthode principale appelée pour transformer l'élément HTML ciblé par le Tag Helper.
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
            if (this.For == null)
            {
                throw new InvalidOperationException("Le modèle n'a pas pu être récupéré.");
            }

            var metadata = this.For.Metadata;
            var propertyName = metadata.PropertyName ?? throw new InvalidOperationException("Impossible de récupérer le nom de la propriété.");
            var displayName = metadata.DisplayName ?? propertyName;

            // Vérification si la propriété est requise
            var isRequired = metadata.ValidatorMetadata.Any(v => v is RequiredAttribute);

            // Récupération de la valeur par défaut du modèle
            var defaultValue = this.For.ModelExplorer.Model?.ToString() ?? string.Empty;

            // Génération du label
            var label = this.htmlGenerator.GenerateLabel(
                this.ViewContext,
                this.For.ModelExplorer,
                this.For.Name,
                labelText: displayName, // Texte personnalisé pour le label
                htmlAttributes: new { @class = "col-form-label" });

            // Génération de l'input avec la valeur par défaut
            var input = this.htmlGenerator.GenerateTextBox(
                this.ViewContext,
                this.For.ModelExplorer,
                this.For.Name,
                value: defaultValue, // Valeur par défaut explicitement passée
                format: null,
                htmlAttributes: new { @class = "form-control" });

            // Génération du message de validation
            var validationMessage = this.htmlGenerator.GenerateValidationMessage(
                this.ViewContext,
                this.For.ModelExplorer,
                this.For.Name, // Nom de la propriété
                message: null, // Message personnalisé (null pour utiliser celui du ModelState)
                tag: null, // Balise HTML à utiliser (par défaut : <span>)
                htmlAttributes: new { @class = "text-danger" });

            // Transformation en structure Bootstrap
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "row g-3 align-items-center mb-3");
            output.Content.SetHtmlContent($@"
                <div class='col-sm-2'>
                    {label.GetString()}{(isRequired ? "<label class='text-danger'>*</label>" : string.Empty)}
                </div>
                <div class='col-sm-10'>
                    {input.GetString()}
                    {validationMessage.GetString()}
                </div>");
        }
    }
}