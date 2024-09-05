import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { Ticket } from './ticket.entity';

@Injectable()
export class TicketService {
  constructor(@InjectRepository(Ticket) private ticketRepository: Repository<Ticket>) {}

  async createTicket(ticketData: Ticket): Promise<Ticket> {
    const newTicket = this.ticketRepository.create(ticketData);
    return await this.ticketRepository.save(newTicket);
  }

  async getTicketById(id: number): Promise<Ticket> {
    return await this.ticketRepository.findOneBy({ id });
  }

  async getAllTickets(): Promise<Ticket[]> {
    return await this.ticketRepository.find();
  }

  async updateTicket(id: number, ticketData: Ticket): Promise<Ticket> {
    const ticket = await this.ticketRepository.findOneBy({ id });
    if (!ticket) {
      throw new Error("Ticket with id ${id} not found.");
    }
    return await this.ticketRepository.save({ ...ticket, ...ticketData });
  }

  async deleteTicket(id: number): Promise<void> {
    const ticket = await this.ticketRepository.findOneBy({ id });
    if (!ticket) {
      throw new Error("Ticket with id ${id} not found.");
    }
    await this.ticketRepository.delete(id);
  }
}
