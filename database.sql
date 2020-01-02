drop table pegawai cascade constraint purge;
drop table barang cascade constraint purge;
drop table buyer cascade constraint purge;
drop table supplier cascade constraint purge;
drop table jenis_barang cascade constraint purge;
drop table htrans_in cascade constraint purge;
drop table dtrans_in cascade constraint purge;
drop table htrans_out cascade constraint purge;
drop table dtrans_out cascade constraint purge;
drop table pengiriman cascade constraint purge;
drop table gudang cascade constraint purge;
drop table mobil cascade constraint purge;
drop table history_perubahan cascade constraint purge;
drop view headerSuratJalan;
drop view detailSuratJalan;
drop view historyHarga;
drop view historybarang;

create table pegawai (
	id_pegawai varchar2(6) primary key, --- substr(jabatan,1,3) + autogenerate
	nama_pegawai varchar2(255),
	jabatan varchar2(15),
	alamat_pegawai varchar2(255),
	password varchar2(255),
	nomor_telp varchar2(13),
	status_pegawai number --- 0:dihapus, 1: aktif
);

create table jenis_barang (
	id_jenis_barang varchar2(6) primary key, --- substr(nama_jenis_barang,1,2) + autogenerate
	nama_jenis_barang varchar2(255)
);

create table gudang (
	id_gudang varchar2(6) primary key, --- GD + autogenerate
	lokasi_gudang varchar2(255)
);

create table mobil (
	id_mobil varchar2(6) primary key,
	plat_nomor varchar2(9) unique,
	nama_mobil varchar2(255),
	kapasitas number
);

create table barang (
	id_barang varchar2(8) primary key,
	id_jenis_barang varchar2(6) constraint fk_idJenis references jenis_barang(id_jenis_barang),
	id_gudang varchar2(6) constraint fk_idGud references gudang(id_gudang),
	nama_barang varchar2(255),
	warna_barang varchar2(255),
	ukuran varchar2(3),
	stock number,
	harga_beli number,
	harga_jual number
);

create table buyer (
	id_buyer varchar2(6) primary key, --- BY + autogenerate
	nama_buyer varchar2(255),
	alamat_buyer varchar2(255),
	email_buyer varchar2(255),
	jenis_buyer varchar2(10),
	status_buyer number --- 0:dihapus, 1: aktif
);

create table supplier (
	id_supplier varchar2(6) primary key, --- SP + autogenerate
	nama_supplier varchar2(255),
	alamat_supplier varchar2(255),
	email_supplier varchar2(255),
    status_delete number 
);

create table pengiriman (
	id_hPengiriman varchar2(15) primary key,
	tanggal_pengiriman date,
	id_supir varchar2(6) constraint fk_pegKirim references pegawai(id_pegawai),
	id_mobil varchar2(6) constraint fk_mobilKirim references mobil(id_mobil)
);

create table htrans_in (
	id_htrans_in varchar2(15) primary key, --- HI + DD + MM + YY + autogenerate
	id_supplier varchar2(6) constraint fk_idSupp references supplier(id_supplier),
	id_gudang varchar2(6) constraint fk_gudHin references gudang(id_gudang),
	tanggal_trans date,
	total_harga number,
	id_nota varchar(50)
);

create table dtrans_in (
	id_htrans_in varchar2(15) constraint fk_Hin references htrans_in(id_htrans_in),
	id_barang varchar2(8) constraint fk_brgHin references barang(id_barang),
	stock_masuk number,
	harga_beli number,
	subtotal number,
	total_stock number,
	id_penanggungjawab varchar(6) constraint fk_pegHin references pegawai(id_pegawai) --- pengurus
);

create table htrans_out (
	id_htrans_out varchar2(15) primary key, --- HO + DD + MM + YY + autogenerate
	id_buyer varchar2(6) constraint fk_idBuy references buyer(id_buyer),
	tanggal_trans date,
	total_harga number,
    total_laba number
);

create table dtrans_out (
	id_htrans_out varchar2(15) constraint fk_Hout references htrans_out(id_htrans_out),
	id_barang varchar2(8) constraint fk_brgHout references barang(id_barang),
	stock_keluar number,
	harga_jual number,
	subtotal number,
	laba number,
	id_penanggungjawab varchar2(6) constraint fk_pegHout references pegawai(id_pegawai), --- pengurus
	id_hPengiriman varchar2(15) constraint fk_pengKirim references pengiriman(id_hPengiriman)
);


create table history_perubahan(
    id_perubahan varchar2(15) primary key,
    id_barang varchar(8) constraint fk_brghp references barang(id_barang),
    tanggal_perubahan date,
    jenis_perubahan varchar2(12),
    stock_awal number,
    stock_baru  number,
    harga_beli_awal number,
    harga_beli_baru number,
    harga_jual_awal number,
    harga_jual_baru number,
    deskripsi varchar2(255),
    id_pegawai varchar(6) constraint fk_pegHp references pegawai(id_pegawai)
);


insert into pegawai values('ADMIN','Admin','Admin','-','admin','-',1);

insert into mobil values('KD0001','L1234KL','Hilux',20);
insert into mobil values('KD0002','L6834ZX','Grand Max',15);
insert into mobil values('KD0003','L7089IO','L3000',18);

insert into jenis_barang values('KE0001','Kemeja');
insert into jenis_barang values('KA0001','Kaos');
insert into jenis_barang values('CP0001','Celana Panjang');
insert into jenis_barang values('CP0002','Celana Pendek');
insert into jenis_barang values('KP0001','Kemeja Panjang');

insert into gudang values('GD0001','Basuki Rahmat 98');
insert into gudang values('GD0002','Keputran 123');
insert into gudang values('GD0003','A Yani 67');

create view headerSuratJalan as
select id_hPengiriman, tanggal_pengiriman, id_mobil, nama_pegawai
from pengiriman p, pegawai peg
where p.id_supir = peg.id_pegawai;

create view detailSuratJalan as
select d.id_htrans_out, rpad(nama_barang, 20) as "NAMA_BARANG", rpad(nama_jenis_barang, 20) as "NAMA_JENIS_BARANG", rpad(warna_barang, 20) as "WARNA_BARANG", ukuran, stock_keluar, rpad(nama_buyer, 20) as "NAMA_BUYER", rpad(alamat_buyer, 20) as "ALAMAT_BUYER", id_hPengiriman
from dtrans_out d, htrans_out h, barang brg, jenis_barang jb, buyer b
where d.id_htrans_out = h.id_htrans_out and d.id_barang = brg.id_barang and brg.id_jenis_barang = jb.id_jenis_barang and h.id_buyer = b.id_buyer;

create or replace view historyHarga as
select * from history_perubahan where (jenis_perubahan='Penyesuaian' or jenis_perubahan='Beli') and id_perubahan not in(select id_perubahan from history_perubahan where harga_jual_baru=harga_jual_awal  and harga_beli_awal=harga_beli_baru)  ;

create or replace view historybarang as
select * from history_perubahan where (jenis_perubahan='Penyesuaian' or jenis_perubahan='Beli') and id_perubahan not in(select id_perubahan from history_perubahan where stock_awal=stock_baru);

commit;

