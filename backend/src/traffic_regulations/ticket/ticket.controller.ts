import { Controller, Get, Post, Body, Param, Put, Delete } from '@nestjs/common';
import { TicketService } from './ticket.service';
import { Ticket } from './ticket.entity';

@Controller('tickets')
export class TicketController {
  constructor(private ticketService: TicketService) {}

  @Post()
  async createTicket(@Body() ticketData: Ticket): Promise<Ticket> {
    return await this.ticketService.createTicket(ticketData);
  }

  @Get(':id')
  async getTicket(@Param('id') id: number): Promise<Ticket> {
    return await this.ticketService.getTicketById(id);
  }

  @Get()
  async getAllTickets(): Promise<Ticket[]> {
    return await this.ticketService.getAllTickets();
  }

  @Put(':id')
  async updateTicket(@Param('id') id: number, @Body() ticketData: Ticket): Promise<Ticket> {
    return await this.ticketService.updateTicket(id, ticketData);
  }

  @Delete(':id')
  async deleteTicket(@Param('id') id: number): Promise<void> {
    return await this.ticketService.deleteTicket(id);
  }
}
