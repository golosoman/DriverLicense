import { IsString, IsNotEmpty, IsOptional } from 'class-validator';
import { ApiProperty } from '@nestjs/swagger';

export class UpdateSignDto {
    @IsString()
    @IsOptional()
    @ApiProperty()
    sprite: string;
  }