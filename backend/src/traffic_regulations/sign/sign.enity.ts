import { Entity, Column, PrimaryGeneratedColumn } from 'typeorm';

@Entity()
export class Sign {
  @PrimaryGeneratedColumn()
  id: number;

  @Column()
  sprite: string;
}
