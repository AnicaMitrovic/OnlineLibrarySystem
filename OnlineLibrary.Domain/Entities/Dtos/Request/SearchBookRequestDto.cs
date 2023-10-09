﻿namespace OnlineLibrary.Domain.Entities.Dtos.Request
{
    public class SearchBookRequestDto
    {
        public int? Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Author { get; set; } = string.Empty;
        public string? Publisher { get; set; } = string.Empty;
    }
}