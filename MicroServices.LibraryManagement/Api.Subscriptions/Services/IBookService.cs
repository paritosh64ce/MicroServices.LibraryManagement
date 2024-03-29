﻿using Repository.Books.Domain;

namespace Api.Subscriptions.Services
{
    public interface IBookService
    {
        Task<Book> GetBook(int id);
        Task SubscribeBook(int id);
        Task UnsubscribeBook(int id);

    }
}
