namespace TestProject1.Models.DTOs;

public record TaskStatisticsResponse(
    int Total,
    int Completed,
    int Pending
);