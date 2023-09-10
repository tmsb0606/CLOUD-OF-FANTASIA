using System;

/// <summary>
/// �񋓌^�̔ėp�֐����Ǘ�����N���X
/// </summary>
public static partial class EnumCommon
{
    /// <summary>
    /// �w�肳�ꂽ�������񋓌^�ɕϊ����܂�
    /// </summary>
    /// <typeparam name="T">�񋓌^</typeparam>
    /// <param name="value">�ϊ����镶����</param>
    /// <returns>�񋓌^�̃I�u�W�F�N�g</returns>
    public static T Parse<T>(string value)
    {
        return Parse<T>(value, true);
    }

    /// <summary>
    /// �w�肳�ꂽ�������񋓌^�ɕϊ����܂�
    /// </summary>
    /// <typeparam name="T">�񋓌^</typeparam>
    /// <param name="value">�ϊ����镶����</param>
    /// <param name="ignoreCase">�啶���Ə���������ʂ��Ȃ��ꍇ�� true</param>
    /// <returns>�񋓌^�̃I�u�W�F�N�g</returns>
    public static T Parse<T>(string value, bool ignoreCase)
    {
        return (T)Enum.Parse(typeof(T), value, ignoreCase);
    }

    /// <summary>
    /// �w�肳�ꂽ�������񋓌^�ɕϊ����Đ����������ǂ�����Ԃ��܂�
    /// </summary>
    /// <typeparam name="T">�񋓌^</typeparam>
    /// <param name="value">�ϊ����镶����</param>
    /// <param name="result">�񋓌^�̃I�u�W�F�N�g</param>
    /// <returns>����ɕϊ����ꂽ�ꍇ�� true</returns>
    public static bool TryParse<T>(string value, out T result)
    {
        return TryParse<T>(value, true, out result);
    }

    /// <summary>
    /// �w�肳�ꂽ�������񋓌^�ɕϊ����Đ����������ǂ�����Ԃ��܂�
    /// </summary>
    /// <typeparam name="T">�񋓌^</typeparam>
    /// <param name="value">�ϊ����镶����</param>
    /// <param name="ignoreCase">�啶���Ə���������ʂ��Ȃ��ꍇ�� true</param>
    /// <param name="result">�񋓌^�̃I�u�W�F�N�g</param>
    /// <returns>����ɕϊ����ꂽ�ꍇ�� true</returns>
    public static bool TryParse<T>(string value, bool ignoreCase, out T result)
    {
        try
        {
            result = (T)Enum.Parse(typeof(T), value, ignoreCase);
            return true;
        }
        catch
        {
            result = default(T);
            return false;
        }
    }
}