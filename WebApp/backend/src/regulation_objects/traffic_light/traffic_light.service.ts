import { Injectable } from '@nestjs/common';
import { InjectModel } from '@nestjs/sequelize';
import { Ticket } from '../ticket/ticket.model';
import { TrafficLight } from './traffic_light.model';
import { CreateTrafficLightDto, UpdateTrafficLightDto } from './dto';

@Injectable()
export class TrafficLightService {
  constructor(@InjectModel(TrafficLight) private trafficLightModel: typeof TrafficLight) {}

  async create(createTrafficLightDto: CreateTrafficLightDto): Promise<TrafficLight> {
    const trafficLight = await this.trafficLightModel.create(createTrafficLightDto);
    return trafficLight;
  }

  async findAll(): Promise<TrafficLight[]> {
    return await this.trafficLightModel.findAll({
      include: [Ticket],
    });
  }

  async findOne(id: number): Promise<TrafficLight | null> {
    return await this.trafficLightModel.findByPk(id, {
      include: [Ticket],
    });
  }

  async update(id: number, updateTrafficLightDto: UpdateTrafficLightDto): Promise<TrafficLight | null> {
    const [count, [trafficLight]] = await this.trafficLightModel.update(updateTrafficLightDto, {
      where: { id },
      returning: true,
    });
    return count > 0 ? trafficLight : null;
  }

  async remove(id: number): Promise<void> {
    await this.trafficLightModel.destroy({ where: { id } });
  }
}
