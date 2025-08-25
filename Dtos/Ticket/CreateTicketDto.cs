namespace CinemaSolutionApi.Dtos.Ticket;

public record class CreateTicketDto(
    string IdUser,
    int IdScreening
    );