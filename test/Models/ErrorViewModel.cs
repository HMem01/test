namespace test.Models
{
    // �����, �������������� ������ ������ ��� ����������� ���������� �� ������
    public class ErrorViewModel
    {
        // �������� RequestId ������������ ������������� �������, ��������� � �������
        public string? RequestId { get; set; }

        // �������� ShowRequestId ���������� ��������, �����������, ������� �� ���������� ������������� �������
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
