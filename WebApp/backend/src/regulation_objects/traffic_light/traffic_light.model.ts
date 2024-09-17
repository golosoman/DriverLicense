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
    type: DataType.ENUM('West', 'East', 'North', 'South'),
    allowNull: false,
  })
  sidePosition: string;

  @Column({
    type:DataType.ENUM('Red', 'Green', 'Yellow', 'Initial'),
    allowNull: false,
  })
  state: string;

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
