﻿namespace MoviesApi.DTOS
{
    public class GenreDto
    {
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
