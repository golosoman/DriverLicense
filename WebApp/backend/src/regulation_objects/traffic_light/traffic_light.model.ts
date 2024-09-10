import {
  Column,
  DataType,
  Model,
  Table,
} from 'sequelize-typescript';

@Table({
  tableName: 'trafficLights',
  timestamps: true
})
export class TrafficLight extends Model<TrafficLight> {
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

  // @Column({
  //   type: DataType.STRING,
  //   allowNull: false,
  // })
  // cycle: string;

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
