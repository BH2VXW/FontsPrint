using System;

namespace Biz126.BlazorUI.Models
{
    public class ErrorViewModel
    {
#pragma warning disable CS8618 // ���˳����캯��ʱ������Ϊ null �� ���ԡ�RequestId����������� null ֵ���뿼�ǽ� ���� ����Ϊ����Ϊ null��
        public string RequestId { get; set; }
#pragma warning restore CS8618 // ���˳����캯��ʱ������Ϊ null �� ���ԡ�RequestId����������� null ֵ���뿼�ǽ� ���� ����Ϊ����Ϊ null��

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}