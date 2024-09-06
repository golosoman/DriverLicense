import { Module } from '@nestjs/common';
import { TicketController } from './ticket.controller';
import { TicketService } from './ticket.service';
import { Ticket } from './ticket.model';
import { SequelizeModule } from '@nestjs/sequelize';
import { TicketCar } from './ticket_cars.model';
import { TicketSign } from './ticket_signs.model';
import { TicketTrafficLight } from './ticket_traffic_lights.model';

@Module({
  imports: [SequelizeModule.forFeature([Ticket, TicketCar, TicketSign, TicketTrafficLight])],
  controllers: [TicketController],
  providers: [TicketService],
})
export class TicketModule {}
