﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFDataModels;

namespace SystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSourceController : ControllerBase
    {
        private readonly EFSystemContext _context;

        public DataSourceController(EFSystemContext context)
        {
            _context = context;
        }

        // GET: api/DataSource
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataSourceTable>>> GetDataSources()
        {
            return await _context.DataSources.ToListAsync();
        }

        // GET: api/DataSource/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataSourceTable>> GetDataSourceTable(Guid id)
        {
            var dataSourceTable = await _context.DataSources.FindAsync(id);

            if (dataSourceTable == null)
            {
                return NotFound();
            }

            return dataSourceTable;
        }

        // PUT: api/DataSource/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataSourceTable(Guid id, [FromBody] DataSourceTable dataSourceTable)
        {
            var dataSource = await _context.DataSources.FindAsync(id);

            if (dataSource == null)
            {
                return NotFound();
            }

            dataSource.DataSourceName = dataSourceTable.DataSourceName;
            dataSource.Configuration = dataSourceTable.Configuration;
            dataSource.ConnectionString = dataSourceTable.ConnectionString;
            dataSource.LastUpdated = DateTime.Now;

            _context.Entry(dataSource).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataSourceTableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DataSource
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<DataSourceTable>> PostDataSourceTable([FromBody] DataSourceTable dataSourceTable)
        {
            // Check if User exists
            var userTable = await _context.Users.FindAsync(dataSourceTable.UserId);
            if(userTable == null)
            {
                return NotFound();
            }

            //Create new Datasource table
            DataSourceTable newDataSource = new DataSourceTable
            {
                DataSourceId = new Guid(),
                DataSourceName = dataSourceTable.DataSourceName,
                Configuration = dataSourceTable.Configuration,
                ConnectionString = dataSourceTable.ConnectionString,
                UserId = dataSourceTable.UserId,
                User = userTable,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now                
            };

            //Add and save changes
            _context.DataSources.Add(newDataSource);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDataSourceTable", new { id = newDataSource.DataSourceId }, newDataSource);
        }

        // DELETE: api/DataSource/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataSourceTable>> DeleteDataSourceTable(Guid id)
        {
            var dataSourceTable = await _context.DataSources.FindAsync(id);
            if (dataSourceTable == null)
            {
                return NotFound();
            }

            _context.DataSources.Remove(dataSourceTable);
            await _context.SaveChangesAsync();

            return dataSourceTable;
        }

        private bool DataSourceTableExists(Guid id)
        {
            return _context.DataSources.Any(e => e.DataSourceId == id);
        }

    }
}
