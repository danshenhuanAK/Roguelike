using UnityEngine;
using TMPro;
using System;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterUser : MonoBehaviour
{
    private MySqlConnection connection;

    private string serverName = "localhost";                //����
    private string abName = "unitygame";                    //���ݿ���
    private string databaseName = "root";                   //���ݿ��û���
    private string databasePassword = "20020302";           //���ݿ�����
    private string port = "3306";                           //mysql����˿ں�

    public GameObject registerUI;                           //ע��UI
    public GameObject loginUI;                              //��¼UI

    public TMP_InputField loginInputUsername;                   //��¼�û��������
    public TMP_InputField loginInputPassword;                   //��¼���������
    public TMP_InputField registerInputUsername;                //ע���û��������
    public TMP_InputField registerInputPassword;                //ע�����������
    public TMP_InputField inputPasswordCompare;                 //ע������ȶԿ�

    public TMP_Text registerReminder;                           //ע����ʾ��Ϣ
    public TMP_Text loginReminder;                              //��¼��ʾ��Ϣ
    public TMP_Text loginUsernameReminder;                      //��¼�˺���ʾ��Ϣ
    public TMP_Text loginPasswordReminder;                      //��¼������ʾ��Ϣ
    public TMP_Text registerUsernameReminder;                   //ע���˺���ʾ��Ϣ
    public TMP_Text registerPasswordReminder;                   //ע��������ʾ��Ϣ
    public TMP_Text PasswordCompareReminder;                    //����ȶ���ʾ��Ϣ

    private void Start()
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);

        string connectionString = "Server=" + serverName + ";Database=" + abName + ";Uid=" + databaseName
                                  + ";Pwd=" + databasePassword + ";Port=" + port + ";";

        connection = new MySqlConnection(connectionString);
        connection.Open();
        Debug.Log("�������ݿ�ɹ�");
    }

    private void Update()
    {
        if (loginUI.activeSelf == true)
        {
            if (loginInputUsername.text.Length >= loginInputUsername.characterLimit)
            {
                loginUsernameReminder.text = "�������" + loginInputUsername.characterLimit + "������";
            }
            else
            {
                loginUsernameReminder.text = "";
            }

            if (loginInputPassword.text.Length >= loginInputPassword.characterLimit)
            {
                loginPasswordReminder.text = "�������" + loginInputPassword.characterLimit + "������";
            }
            else
            {
                loginPasswordReminder.text = "";
            }
        }
        else if (registerUI.activeSelf == true)
        {
            if (registerInputUsername.text.Length >= registerInputUsername.characterLimit)
            {
                registerUsernameReminder.text = "�������" + registerInputUsername.characterLimit + "������";
            }
            else
            {
                registerUsernameReminder.text = "";
            }

            if (registerInputPassword.text.Length >= registerInputPassword.characterLimit)
            {
                registerPasswordReminder.text = "�������" + registerInputPassword.characterLimit + "������";
            }
            else
            {
                registerPasswordReminder.text = "";
            }

            if (inputPasswordCompare.text.Length == 0)
            {
                PasswordCompareReminder.text = "";
            }
            else
            {
                if (inputPasswordCompare.text.Equals(registerInputPassword.text))
                {
                    PasswordCompareReminder.text = "����ȶ���ȷ";
                }
                else
                {
                    PasswordCompareReminder.text = "����ȶԴ���";
                }
            }
        }
    }

    public void Register()                      //ע��
    {
        string username = registerInputUsername.text;
        string userpassword = HashPassword(registerInputPassword.text);

        if (username == "" || userpassword == "")
        {
            registerReminder.text = "�˺Ż����벻��Ϊ��";
        }
        else
        {
            string query1 = "SELECT COUNT(*) FROM userinfo WHERE username = @username";

            MySqlCommand cmd1 = new MySqlCommand(query1, connection);
            cmd1.Parameters.AddWithValue("@username", username);

            int count = Convert.ToInt32(cmd1.ExecuteScalar());

            if (count > 0)
            {
                Debug.Log("�û����Ѵ��ڣ���ѡ��ͬ���û���!");
                registerReminder.text = "�û����Ѵ��ڣ���ѡ��ͬ���û���!";
            }
            else
            {
                string query2 = "INSERT INTO userinfo(username,password) VALUES (@username, @password)";

                MySqlCommand cmd2 = new MySqlCommand(query2, connection);
                cmd2.Parameters.AddWithValue("@username", username);
                cmd2.Parameters.AddWithValue("@password", userpassword);

                int rowsAffected = cmd2.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Debug.Log("ע��ɹ�");
                    registerReminder.text = "ע��ɹ�";
                }
                else
                {
                    Debug.Log("ע��ʧ��");
                    registerReminder.text = "ע��ɹ�";
                }
            }

            registerInputUsername.text = "";
            registerInputPassword.text = "";
            inputPasswordCompare.text = "";
        }
    }

    public void Login()
    {
        string username = loginInputUsername.text;
        string userpassword = HashPassword(loginInputPassword.text);
        string query = "SELECT COUNT(*) FROM userinfo WHERE username = @username AND password = @password";

        MySqlCommand cmd = new MySqlCommand(query, connection);

        cmd.Parameters.AddWithValue("username", username);
        cmd.Parameters.AddWithValue("password", userpassword);

        object result = cmd.ExecuteScalar();
        int count = Convert.ToInt32(result);
        Debug.Log(1);
        if (count > 0)
        {
            Debug.Log("��¼�ɹ�");
            loginReminder.text = "��¼�ɹ�";
            SceneManager.LoadScene(1);
        }
        else
        {
            string errorMessage;

            query = "SELECT COUNT(*) FROM userinfo WHERE username = @username";

            cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@username", username);
            result = cmd.ExecuteScalar();
            count = Convert.ToInt32(result);

            if (count == 0)
            {
                errorMessage = "�û���������";

                loginInputUsername.text = "";
                loginInputPassword.text = "";
            }
            else
            {
                errorMessage = "�������";

                loginInputPassword.text = "";
            }

            Debug.Log("��¼ʧ�ܣ�" + errorMessage);
            loginReminder.text = errorMessage;
        }
    }

    // ��������
    private static string HashPassword(string password)
    {
        SHA256Managed crypt = new SHA256Managed();
        StringBuilder hash = new StringBuilder();

        byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password));

        foreach (byte theByte in crypto)
        {
            hash.Append(theByte.ToString("x2"));
        }

        return hash.ToString();
    }
}
