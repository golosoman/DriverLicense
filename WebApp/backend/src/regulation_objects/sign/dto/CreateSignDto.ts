import { IsString, IsNotEmpty, IsOptional } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class CreateSignDto {
  @IsString()
  @IsNotEmpty()
  @ApiProperty()
  modelName: string;

  @IsString()
  @IsOptional()
  @ApiProperty()
  sidePosition: string;
}