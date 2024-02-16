using DomainLayer.Models;
using System.Security.Cryptography;

namespace DomainServiceLayer.Utils
{
    public abstract class HashPassword
    {
        public static bool VerifyPassword(User user, string inputPassword)
        {
            // Converte a senha armazenada do usuário de uma string Base64 para um array de bytes
            byte[] storedHashBytes = Convert.FromBase64String(user.Password);

            // Cria um novo array de bytes para armazenar os dados a mais inseridos na senha 
            byte[] salt = new byte[16];

            // Copia os primeiros 16 bytes do array hashBytes para o array salt
            Array.Copy(storedHashBytes, 0, salt, 0, 16);

            // Cria um novo objeto Rfc2898DeriveBytes usando a senha inserida e o salt
            // O número 1000 é o número de iterações do algoritmo PBKDF2 irá fazer
            var pbkdf2 = new Rfc2898DeriveBytes(inputPassword, salt, 1000);

            // Obtém um hash de 20 bytes da senha fornecida pelo usuario
            byte[] inputHash = pbkdf2.GetBytes(20);

            // Cria um novo array de bytes para armazenar o hash da senha armazenada
            byte[] storedHash = new byte[20];

            // Copia os 20 bytes do hash da senha armazenada para o array storedHash
            Array.Copy(storedHashBytes, 16, storedHash, 0, 20);

            // Compara o hash da senha inserida com o hash da senha armazenada
            // Se todos os bytes corresponderem, retorna true
            return storedHash.SequenceEqual(inputHash);

        }

        public static string EncriptPassword(User user)
        {
            //Gera numeros aleatorios
            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            
            //gera caracteres aleatorios
            byte[] salt = new byte[16];
            rngCsp.GetBytes(salt);

            //numero de interações para que o algoritimo possa criar o password
            var pbkdf2 = new Rfc2898DeriveBytes(user.Password, salt, 1000);

            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string hashPassword = Convert.ToBase64String(hashBytes);

            return hashPassword;
        }
    }
}
