﻿namespace OnlineLibrary.Domain.Entities.Dtos.Request
{
    public class BookRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
    }
}
