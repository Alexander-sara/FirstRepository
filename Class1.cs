﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Shifrator
{
    class Program
    {
        //функция шифрования
        static void EncryptFile(string originalFile, string encryptedFile, string keyFile)
        {
            // Чтение байтов исходного файла
            byte[] originalBytes;
            using (FileStream fs = new FileStream(originalFile, FileMode.Open))
            {
                originalBytes = new byte[fs.Length];
                fs.Read(originalBytes, 0, originalBytes.Length);
            }
            //Создаем одноразовый ключ для шифрования
            byte[] keyBytes = new byte[originalBytes.Length];
            Random random = new Random();
            random.NextBytes(keyBytes);
            //Записываем ключ в файл
            using (FileStream fs = new FileStream(keyFile, FileMode.Create))
            {
                fs.Write(keyBytes, 0, keyBytes.Length);
            }
            // Шифрование данных с помощью алгоритма Вернама
            byte[] encryptedBytes = new byte[originalBytes.Length];
            DoShifr(originalBytes, keyBytes, ref encryptedBytes);
            //Записываем зашифрованный файл
            using (FileStream fs = new FileStream(encryptedFile, FileMode.Create))
            {
                fs.Write(encryptedBytes, 0, encryptedBytes.Length);
            }
        }
        //функция дешифрования
        static void DecryptFile(string encryptedFile, string keyFile, string decryptedFile)
        {
            // Чтение в зашифрованных байтов
            byte[] encryptedBytes;
            using (FileStream fs = new FileStream(encryptedFile, FileMode.Open))
            {
                encryptedBytes = new byte[fs.Length];
                fs.Read(encryptedBytes, 0, encryptedBytes.Length);
            }
            // Чтение в ключе
            byte[] keyBytes;
            using (FileStream fs = new FileStream(keyFile, FileMode.Open))
            {
                keyBytes = new byte[fs.Length];
                fs.Read(keyBytes, 0, keyBytes.Length);
            }
            // Расшифровывать данные с помощью алгоритма Вернама
            byte[] decryptedBytes = new byte[encryptedBytes.Length];
            DoShifr(encryptedBytes, keyBytes, ref decryptedBytes);
            //Записываем дешифрованный файл
            using (FileStream fs = new FileStream(decryptedFile, FileMode.Create))
            {
                fs.Write(decryptedBytes, 0, decryptedBytes.Length);
            }
        }
        static void DoShifr(byte[] inBytes, byte[] keyBytes, ref byte[] outBytes)
        {
            //Проверка аргументов
            if ((inBytes.Length != keyBytes.Length) || (keyBytes.Length != outBytes.Length))
                throw new ArgumentException("Байт - массив не имеет одинаковой длины!");
                // Шифрование / дешифрование с помощью XOR:
            for (int i = 0; i < inBytes.Length; i++)
                outBytes[i] = (byte)(inBytes[i] ^ keyBytes[i]);
        }
    }
}
