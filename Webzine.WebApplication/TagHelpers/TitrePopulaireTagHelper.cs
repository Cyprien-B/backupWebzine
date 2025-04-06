// <copyright file="TitrePopulaireTagHelper.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.TagHelpers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    /// <summary>
    /// TagHelper permettant d'afficher la card d'un titre populaire à partir du modèle lié via l'attribut asp-for.
    /// </summary>
    [HtmlTargetElement("titre-populaire", Attributes = "asp-for")]
    public class TitrePopulaireTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory urlHelperFactory;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="TitrePopulaireTagHelper"/>.
        /// </summary>
        /// <param name="urlHelperFactory">Factory pour la création d'IUrlHelper.</param>
        public TitrePopulaireTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }

        /// <summary>
        /// Obtient ou définit la liaison des propriétés du modèle via l'attribut asp-for.
        /// </summary>
        [HtmlAttributeName("asp-for")]
        public ModelExpression? For { get; set; }

        /// <summary>
        /// Obtient ou définit le contexte de la vue.
        /// </summary>
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; } = null!;

        /// <summary>
        /// Méthode principale appelée pour transformer l'élément HTML ciblé par le TagHelper.
        /// </summary>
        /// <param name="context">Contexte du TagHelper.</param>
        /// <param name="output">Sortie du TagHelper.</param>
        /// <exception cref="InvalidOperationException">Levée si le modèle lié via asp-for est null.</exception>
        /// <exception cref="ArgumentException">Levée si le modèle lié à asp-for est invalide.</exception>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            this.ValidateModel();
            var titre = (dynamic)this.For!.Model;
            var urlHelper = this.urlHelperFactory.GetUrlHelper(this.ViewContext);

            output.TagName = "div";
            output.Attributes.SetAttribute("class", "col");

            var card = new TagBuilder("div");
            card.AddCssClass("card");

            card.InnerHtml.AppendHtml(this.BuildImageContainer(titre, urlHelper));
            card.InnerHtml.AppendHtml(this.BuildCardBody(titre, urlHelper));

            output.Content.SetHtmlContent(card);
        }

        private void ValidateModel()
        {
            if (this.For == null)
            {
                throw new InvalidOperationException("Le modèle n'a pas pu être récupéré.");
            }

            if (this.For.Model == null)
            {
                throw new ArgumentException("Le modèle lié à 'asp-for' est null ou invalide.");
            }
        }

        private TagBuilder BuildImageContainer(dynamic titre, IUrlHelper urlHelper)
        {
            var container = new TagBuilder("div");
            container.AddCssClass("ratio ratio-1x1");

            var link = new TagBuilder("a");
            link.Attributes["href"] = urlHelper.Action("Index", "Titre", new { id = titre.IdTitre });

            var img = new TagBuilder("img");
            img.Attributes["src"] = titre.UrlJaquette;
            img.Attributes["onerror"] = "this.src='/images/JaquetteDefault.png'";
            img.AddCssClass("card-img-top object-fit-cover h-100 w-100");

            link.InnerHtml.AppendHtml(img);
            container.InnerHtml.AppendHtml(link);

            return container;
        }

        private TagBuilder BuildCardBody(dynamic titre, IUrlHelper urlHelper)
        {
            var body = new TagBuilder("div");
            body.AddCssClass("card-body");

            // Titre
            var titleLink = new TagBuilder("a");
            titleLink.Attributes["href"] = urlHelper.Action("Index", "Titre", new { id = titre.IdTitre });
            titleLink.AddCssClass("text-decoration-none");
            titleLink.InnerHtml.Append(titre.Libelle);

            var title = new TagBuilder("h5");
            title.AddCssClass("card-title mb-2");
            title.InnerHtml.AppendHtml(titleLink);

            // Artiste
            var artistLink = new TagBuilder("a");
            artistLink.Attributes["href"] = urlHelper.Action("Index", "Artiste", new { artiste = titre.Artiste.Nom });
            artistLink.AddCssClass("text-decoration-none");
            artistLink.InnerHtml.Append(titre.Artiste.Nom);

            var artistText = new TagBuilder("p");
            artistText.AddCssClass("card-text mb-0 text-muted");
            artistText.InnerHtml.Append("par ");
            artistText.InnerHtml.AppendHtml(artistLink);

            body.InnerHtml.AppendHtml(title);
            body.InnerHtml.AppendHtml(artistText);

            return body;
        }
    }
}