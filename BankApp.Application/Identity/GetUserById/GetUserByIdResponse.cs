﻿namespace BankApp.Application.Identity
{
    public class GetUserByIdResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
    }
}