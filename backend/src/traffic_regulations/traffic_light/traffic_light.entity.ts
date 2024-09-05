import { Entity, Column, PrimaryGeneratedColumn } from 'typeorm';

@Entity()
export class TrafficLight {
  @PrimaryGeneratedColumn()
  id: number;

  @Column()
  sprite: string;

  @Column()
  cycle: string;
}
