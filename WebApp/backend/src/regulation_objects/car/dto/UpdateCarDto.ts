import { IsString, IsOptional, IsNumber } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class UpdateCarDto {
  @IsString()
  @IsOptional()
  @ApiProperty()
  modelName: string;

  @IsString()
  @IsOptional()
  @ApiProperty()
  position: string;

  @IsNumber()
  @IsOptional()
  @ApiProperty()
  speed: number;

  @IsString()
  @IsOptional()
  @ApiProperty()
  movementDirection: string;
}
