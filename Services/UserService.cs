using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Jobs2.Modules;
using System.Linq;
using Jobs2.Services;
using Jobs2.interfaces;

namespace Jobs2.Services
{
    public class UserService : IUserService
    {
        private Jobs2Service Jobs2Service;
        
        public UserService(Jobs2Service Jobs2Service)
        {
            this.Jobs2Service = Jobs2Service;
        }

        List<User> UsersList { get; set; }
        public User currentUser{set; get;}
        private static string fileName = "user.json";

        public UserService()
        {
            using (var jsonFile = File.OpenText(fileName))
            {
                UsersList = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }
        public void Delete(int userId)
        {      
            var user = Get(userId);
            if (user is null)
            {
                return;
            } 
            UsersList.Remove(user);
            saveToFile();
        }
        public void saveToFile()
        {
            File.WriteAllText(fileName, JsonSerializer.Serialize(UsersList));
        }

        public List<User> GetAll() => UsersList;
        public User Get(int id) => UsersList.FirstOrDefault(j => j.Id == id);
        public User GetUser(string name, string password)
        {
            foreach(User u in UsersList)
            {
                if(u.Password==password && u.UserName==name)
                return u;
            }
            return null;
        }

        public User postUser(User user)
        {
            user.Id = Count+10;
                    UsersList.Add(user);
                    saveToFile();  
                

        return    user;
        //GetUser(user.UserName, user.Password);
        }


        public int Count => UsersList.Count();

    }

}
