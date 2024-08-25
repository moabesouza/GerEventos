﻿using GerEventos.Services.Dtos.BalcaoVendas;
using GerEventos.Services.Dtos.TipoEvento;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form.DatePicker;

namespace GerEventos.Services.Dtos.Eventos
{
    public class CreateUpdateEventoDto
    {
        [Required]
        [StringLength(128)]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Tipo de Evento")]
        public Guid TipoEventoId { get; set; } 

        [Required]
        [Display(Name = "Balcão de Vendas")]
        public Guid BalcaoVendasId { get; set; }

        [Required]
        [Display(Name = "Produtor")]
        public Guid ProdutorId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data Início")]
        public DateTime DataInicio { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data Fim")]
        public DateTime DataFim { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

      


    }
}
