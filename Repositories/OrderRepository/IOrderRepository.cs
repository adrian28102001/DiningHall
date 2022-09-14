﻿using DiningHall.Models;

namespace DiningHall.Repositories.OrderRepository;

public interface IOrderRepository
{
    void InsertOrder(Order order);
    IList<Order> GetAll();
    Order? GetById(int id);
    Order? GetOrderByStatus(Status status);

    Order? GetOrderByTableId(int id);
}