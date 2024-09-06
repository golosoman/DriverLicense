import { Injectable } from '@nestjs/common';
import { InjectModel } from '@nestjs/sequelize';
import { Ticket } from '../ticket/ticket.model';
import { Sign } from './sign.model';
import { CreateSignDto, UpdateSignDto } from './dto';

@Injectable()
export class SignService {
  constructor(@InjectModel(Sign) private signModel: typeof Sign) {}

  async create(createSignDto: CreateSignDto): Promise<Sign> {
    const sign = await this.signModel.create(createSignDto);
    return sign;
  }

  async findAll(): Promise<Sign[]> {
    return await this.signModel.findAll({
      include: [Ticket],
    });
  }

  async findOne(id: number): Promise<Sign | null> {
    return await this.signModel.findByPk(id, {
      include: [Ticket],
    });
  }

  async update(id: number, updateSignDto: UpdateSignDto): Promise<Sign | null> {
    const [count, [sign]] = await this.signModel.update(updateSignDto, {
      where: { id },
      returning: true,
    });
    return count > 0 ? sign : null;
  }

  async remove(id: number): Promise<void> {
    await this.signModel.destroy({ where: { id } });
  }
}
