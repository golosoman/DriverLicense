import { Module } from '@nestjs/common';
import { TicketController } from './ticket.controller';
import { TicketService } from './ticket.service';
import { Ticket } from './ticket.model';
import { SequelizeModule } from '@nestjs/sequelize';
import { TicketRoadUser } from './ticketRoadUser.model';
import { TicketSign } from './ticketSign.model';
import { TicketTrafficLight } from './ticketTrafficLight.model';

@Module({
  imports: [SequelizeModule.forFeature([Ticket, TicketRoadUser, TicketSign, TicketTrafficLight])],
  controllers: [TicketController],
  providers: [TicketService],
})
export class TicketModule {}
