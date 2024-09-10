import { Injectable } from '@nestjs/common';
import { InjectModel } from '@nestjs/sequelize';
import { RoadUser } from '../car/roadUsers.model';
import { Sign } from '../sign/sign.model';
import { TrafficLight } from '../traffic_light/traffic_light.model';
import { Ticket } from './ticket.model';
import { TicketRoadUser } from './ticketRoadUser.model';
import { TicketSign } from './ticketSign.model';
import { TicketTrafficLight } from './ticketTrafficLight.model';
import { CreateTicketDto, UpdateTicketDto } from './dto';

@Injectable()
export class TicketService {
  constructor(
    @InjectModel(Ticket) private ticketModel: typeof Ticket,
    @InjectModel(TicketRoadUser) private ticketCarsRepository: typeof TicketRoadUser, 
    @InjectModel(TicketSign) private ticketSignsRepository: typeof TicketSign, 
    @InjectModel(TicketTrafficLight) private ticketTrafficLightsRepository: typeof TicketTrafficLight,
  ) {}

  async create(createTicketDto: CreateTicketDto): Promise<Ticket> {
    const transaction = await this.ticketModel.sequelize.transaction();
  
    try {
      const ticket = await this.ticketModel.create(createTicketDto, { transaction });
  
      // Связывание с машинами
      const roadUserIds = createTicketDto.cars;
      for (const roadUserId of roadUserIds) {
        await this.ticketCarsRepository.create({
          ticketId: ticket.id,
          roadUserId,
        }, { transaction }); 
      }
  
      // Связывание со знаками
      const signIds = createTicketDto.signs;
      for (const signId of signIds) {
        await this.ticketSignsRepository.create({
          ticketId: ticket.id,
          signId,
        }, { transaction }); 
      }
  
      // Связывание со светофорами
      const trafficLightIds = createTicketDto.trafficLights;
      for (const trafficLightId of trafficLightIds) {
        await this.ticketTrafficLightsRepository.create({
          ticketId: ticket.id,
          trafficLightId,
        }, { transaction }); 
      }
  
      await transaction.commit();
      return ticket;
    } catch (error) {
      await transaction.rollback();
      throw error;
    }
  }

  async findAll(): Promise<Ticket[]> {
    return await this.ticketModel.findAll({
      include: [RoadUser, Sign, TrafficLight],
    });
  }

  async test(): Promise<Ticket[]>{
    return await this.ticketModel.findAll({
      offset:1, 
      limit:1, 
    });
  }

  async findOne(id: number): Promise<Ticket | null> {
    return await this.ticketModel.findByPk(id, {
      include: [RoadUser, Sign, TrafficLight],
    });
  }

  async update(id: number, updateTicketDto: UpdateTicketDto): Promise<Ticket | null> {
    const [count, [ticket]] = await this.ticketModel.update(updateTicketDto, {
      where: { id },
      returning: true,
    });
    return count > 0 ? ticket : null;
  }

  async remove(id: number): Promise<void> {
    await this.ticketModel.destroy({ where: { id } });
  }

  

  async findQuestions(offset: number, limit: number): Promise<Ticket[]> {
    return await this.ticketModel.findAll({
      attributes: ['question'], // Выбираем только поле "question"
      offset, // Смещение
      limit, // Лимит количества записей
    });
  }
}
