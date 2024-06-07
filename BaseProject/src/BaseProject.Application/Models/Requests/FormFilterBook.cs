﻿namespace BaseProject.Application.Models.Requests
{
    public class FormFilterBook
    {
        public string? Name { get; set; }
        public string? Author { get; set; }
        public int? ReleaseYearFrom { get; set; }
        public int? ReleaseYearTo { get; set; }
        public string? CategoryName { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}