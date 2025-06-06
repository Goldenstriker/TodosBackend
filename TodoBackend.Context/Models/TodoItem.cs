﻿using System.ComponentModel.DataAnnotations;
using Utility;

namespace TodoBackend.Context.Models
{
    internal class TodoItem : IBaseEntity, IAuditLog
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public Guid TodoId { get; set; }

        public Todo Todo { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
