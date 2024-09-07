import { Injectable } from '@nestjs/common';
import { InjectModel } from '@nestjs/sequelize';
import { Ticket } from '../ticket/ticket.model';
import { Car } from './car.model';
import { CreateCarDto, UpdateCarDto } from './dto';

@Injectable()
export class CarService {
  constructor(@InjectModel(Car) private carModel: typeof Car) {}

  async create(createCarDto: CreateCarDto): Promise<Car> {
    const car = await this.carModel.create(createCarDto);
    return car;
  }

  async findAll(): Promise<Car[]> {
    return await this.carModel.findAll({
      include: [Ticket],
    });
  }

  async findOne(id: number): Promise<Car | null> {
    return await this.carModel.findByPk(id, {
      include: [Ticket],
    });
  }

  async update(id: number, updateCarDto: UpdateCarDto): Promise<Car | null> {
    const [count, [car]] = await this.carModel.update(updateCarDto, {
      where: { id },
      returning: true,
    });
    return count > 0 ? car : null;
  }

  async remove(id: number): Promise<void> {
    await this.carModel.destroy({ where: { id } });
  }
}
