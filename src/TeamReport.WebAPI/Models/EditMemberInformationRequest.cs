﻿namespace TeamReport.WebAPI.Models;

public class EditMemberInformationRequest
{
    public int? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Title { get; set; }
}