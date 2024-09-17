import {
  Column,
  DataType,
  Model,
  Table,
} from 'sequelize-typescript';

@Table({
  tableName: 'signs',
  timestamps: true
})
export class Sign extends Model<Sign> {
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
