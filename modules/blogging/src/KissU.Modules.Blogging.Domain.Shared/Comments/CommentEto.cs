﻿using System;

namespace KissU.Modules.Blogging.Domain.Shared.Comments
{
    [Serializable]
    public class CommentEto
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public Guid? RepliedCommentId { get; set; }

        public string Text { get; set; }
    }
}