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

    private string serverName = "localhost";                //主机
    private string abName = "unitygame";                    //数据库名
    private string databaseName = "root";                   //数据库用户名
    private string databasePassword = "20020302";           //数据库密码
    private string port = "3306";                           //mysql服务端口号

    public GameObject registerUI;                           //注册UI
    public GameObject loginUI;                              //登录UI

    public TMP_InputField loginInputUsername;                   //登录用户名输入框
    public TMP_InputField loginInputPassword;                   //登录密码输入框
    public TMP_InputField registerInputUsername;                //注册用户名输入框
    public TMP_InputField registerInputPassword;                //注册密码输入框
    public TMP_InputField inputPasswordCompare;                 //注册密码比对框

    public TMP_Text registerReminder;                           //注册提示消息
    public TMP_Text loginReminder;                              //登录提示消息
    public TMP_Text loginUsernameReminder;                      //登录账号提示消息
    public TMP_Text loginPasswordReminder;                      //登录密码提示消息
    public TMP_Text registerUsernameReminder;                   //注册账号提示消息
    public TMP_Text registerPasswordReminder;                   //注册密码提示消息
    public TMP_Text PasswordCompareReminder;                    //密码比对提示消息

    private void Start()
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);

        string connectionString = "Server=" + serverName + ";Database=" + abName + ";Uid=" + databaseName
                                  + ";Pwd=" + databasePassword + ";Port=" + port + ";";

        connection = new MySqlConnection(connectionString);
        connection.Open();
        Debug.Log("连接数据库成功");
    }

    private void Update()
    {
        if (loginUI.activeSelf == true)
        {
            if (loginInputUsername.text.Length >= loginInputUsername.characterLimit)
            {
                loginUsernameReminder.text = "最多输入" + loginInputUsername.characterLimit + "个数字";
            }
            else
            {
                loginUsernameReminder.text = "";
            }

            if (loginInputPassword.text.Length >= loginInputPassword.characterLimit)
            {
                loginPasswordReminder.text = "最多输入" + loginInputPassword.characterLimit + "个数字";
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
                registerUsernameReminder.text = "最多输入" + registerInputUsername.characterLimit + "个数字";
            }
            else
            {
                registerUsernameReminder.text = "";
            }

            if (registerInputPassword.text.Length >= registerInputPassword.characterLimit)
            {
                registerPasswordReminder.text = "最多输入" + registerInputPassword.characterLimit + "个数字";
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
                    PasswordCompareReminder.text = "密码比对正确";
                }
                else
                {
                    PasswordCompareReminder.text = "密码比对错误";
                }
            }
        }
    }

    public void Register()                      //注册
    {
        string username = registerInputUsername.text;
        string userpassword = HashPassword(registerInputPassword.text);

        if (username == "" || userpassword == "")
        {
            registerReminder.text = "账号或密码不能为空";
        }
        else
        {
            string query1 = "SELECT COUNT(*) FROM userinfo WHERE username = @username";

            MySqlCommand cmd1 = new MySqlCommand(query1, connection);
            cmd1.Parameters.AddWithValue("@username", username);

            int count = Convert.ToInt32(cmd1.ExecuteScalar());

            if (count > 0)
            {
                Debug.Log("用户名已存在，请选择不同的用户名!");
                registerReminder.text = "用户名已存在，请选择不同的用户名!";
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
                    Debug.Log("注册成功");
                    registerReminder.text = "注册成功";
                }
                else
                {
                    Debug.Log("注册失败");
                    registerReminder.text = "注册成功";
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
            Debug.Log("登录成功");
            loginReminder.text = "登录成功";
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
                errorMessage = "用户名不存在";

                loginInputUsername.text = "";
                loginInputPassword.text = "";
            }
            else
            {
                errorMessage = "密码错误";

                loginInputPassword.text = "";
            }

            Debug.Log("登录失败：" + errorMessage);
            loginReminder.text = errorMessage;
        }
    }

    // 加密密码
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
