import {
  Column,
  DataType,
  Model,
  Table,
} from 'sequelize-typescript';
import { ParticipantType, MovementDirectionType, SidePosition } from './dto';

@Table({
  tableName: 'roadUsers',
  timestamps: true
})
export class RoadUser extends Model<RoadUser> {
  @Column({
    type: DataType.ENUM('passengerCar', 'walker', 'tram'),
    allowNull: false,
  })
  typeParticipant: string;

  @Column({
    type: DataType.STRING,
    allowNull: false,
  })
  modelName: string;

  @Column({
    type: DataType.ENUM('west', 'east', 'north', 'south'),
    allowNull: false,
  })
  sidePosition: string;

  @Column({
    type: DataType.INTEGER,
    allowNull: true,
  })
  numberPosition: number;

  @Column({
    type: DataType.ENUM('forward', 'backward', 'left', 'right'),
    allowNull: false,
  })
  movementDirection: string;

  @Column({
    type: DataType.DATE,
    allowNull: true
  })
  createdAt: Date;

  @Column({
    type: DataType.DATE,
    allowNull: true
  })
  updatedAt: Date;
}
