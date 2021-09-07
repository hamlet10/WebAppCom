using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class OutPut
    {
        public int OutputId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public int ConceptId { get; set; }
        public int BodegaId { get; set; }

        [ForeignKey(nameof(BodegaId))]
        public virtual Bodega Bodega { get; set; }
        [ForeignKey(nameof(ConceptId))]
        public virtual Concept Concept { get; set; }
        [ForeignKey(nameof(OutputId))]
        public virtual OutPutDetails Output { get; set; }
    }
}
