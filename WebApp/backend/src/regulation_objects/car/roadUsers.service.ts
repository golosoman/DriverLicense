import { Injectable } from '@nestjs/common';
import { InjectModel } from '@nestjs/sequelize';
import { Ticket } from '../ticket/ticket.model';
import { RoadUser } from './roadUsers.model';
import { CreateRoadUserDto, UpdateRoadUserDto } from './dto';

@Injectable()
export class RoadUserService {
  constructor(@InjectModel(RoadUser) private roadUserModel: typeof RoadUser) {}

  async create(createRoadUserDto: CreateRoadUserDto): Promise<RoadUser> {
    const roadUser = await this.roadUserModel.create(createRoadUserDto);
    return roadUser;
  }

  async findAll(): Promise<RoadUser[]> {
    return await this.roadUserModel.findAll({
      include: [Ticket],
    });
  }

  async findOne(id: number): Promise<RoadUser | null> {
    return await this.roadUserModel.findByPk(id, {
      include: [Ticket],
    });
  }

  async update(id: number, updateRoadUserDto: UpdateRoadUserDto): Promise<RoadUser | null> {
    const [count, [roadUser]] = await this.roadUserModel.update(updateRoadUserDto, {
      where: { id },
      returning: true,
    });
    return count > 0 ? roadUser : null;
  }

  async remove(id: number): Promise<void> {
    await this.roadUserModel.destroy({ where: { id } });
  }
}
