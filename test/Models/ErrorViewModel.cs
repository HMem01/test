namespace test.Models
{
    //  ласс, представл€ющий модель данных дл€ отображени€ информации об ошибке
    public class ErrorViewModel
    {
        // —войство RequestId представл€ет идентификатор запроса, св€занный с ошибкой
        public string? RequestId { get; set; }

        // —войство ShowRequestId возвращает значение, указывающее, следует ли отображать идентификатор запроса
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
